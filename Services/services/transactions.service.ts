import Moleculer, { Errors as MolErrors, ServiceSchema, ServiceSettingSchema } from 'moleculer'
import { AppDataSource, User } from '../data/data-sources'
import { Currency } from '../entities/Currency'
import { Transaction } from '../entities/Transaction'


module.exports = {

    name: "transations",
    version: 1,

    actions: {

        create:{

            rest: "POST /create-transaction",
            
            params:{
                userId: 'string',

                baseCurrencyId: 'number',
                changeCurrencyId: 'number',
                amount: { type: 'number', positive: 'true' },
            },

            async handler(ctx){

                //verify Currencies
                const currenciesRepos = AppDataSource.getRepository(Currency)
                
                
                // use 'mcall' to simplify
                const baseCurrency: Currency | null = await ctx.call('v1.currencies.getById', { id: ctx.params.baseCurrencyId })
                const changeCurrency: Currency | null = await ctx.call('v1.currencies.getById', { id: ctx.params.changeCurrencyId })
                const user: User = await ctx.call('v1.users.getById', { id: ctx.params.userId })

                if(!user){
                    throw new MolErrors.MoleculerClientError('invalid user provided', 400)
                }
                
                if(!baseCurrency){
                    throw new MolErrors.MoleculerClientError('base currency not found', 400)
                }
                if(!changeCurrency){
                    throw new MolErrors.MoleculerClientError('change currency not found', 400)
                }


                //All okay

                let transaction = new Transaction();
                transaction.baseCurrency = baseCurrency.symbol!
                transaction.changeCurrency = changeCurrency.symbol!
                transaction.transactionActor = user,
                transaction.amount = ctx.params.amount,
                transaction.transactionDate = Date.now()
                transaction.status = 'accepted'

                transaction = await AppDataSource.manager.save(Transaction, transaction);
                
                if(transaction){
                    return transaction
                }

                throw new MolErrors.MoleculerClientError('internal server error', 500)
            }

        },

        list: {
            rest: "GET /list/:userId",

            cache: true,
            
            params: {
                userId: "string"
            },

            async handler(ctx){
                
                //find user
                const user = await AppDataSource.getRepository(User).findOne({
                    where:  {
                        id: ctx.params.userId
                    },
                    relations: {
                        transactions: true
                    }
                });

                if(!user){
                    return new Moleculer.Errors.MoleculerClientError('transaction actor not found', 404)
                }
                
                if(user.transactions)
                    return user.transactions;
                return [];
            }
        }
    }


} satisfies ServiceSchema<ServiceSettingSchema>