import Moleculer, { ServiceSchema, ServiceSettingSchema } from 'moleculer';
import { AppDataSource, Currency } from '../data/data-sources'

module.exports = {

    name: "currencies",
    version: 1,

    actions:{

        add: {
            rest: "POST /add",
            
            params: {
                name: 'string',
                symbol: 'string',
                valueProviderUrl: { type: 'url', optional: true  }
            },

            async handler(ctx){

                const c = new Currency();
                c.name = ctx.params.name
                c.symbol =  ctx.params.symbol
                c.valueProviderUrl = ctx.params.valueProviderUrl ?? ""

                return await AppDataSource.manager.save(Currency, c)
            }
        },

        delete: {
            rest: 'DELETE /delete',

            params: {
                id: 'string'
            },

            async handler(ctx){

                const currenciesRepos = AppDataSource.getRepository(Currency);
               
                const requestedCurrency = await currenciesRepos.findOneBy({
                    id: Number(ctx.params.id)
                });

                if(!requestedCurrency){
                    throw new Moleculer.Errors.MoleculerClientError('currency not found', 404)
                }

                await currenciesRepos.remove(requestedCurrency);

                return 'resource deleted';
            }
        },

        update: {

            rest: "PUT /update/:id",

            params: {
                id: 'string',
                name:{ type: 'string', optional: true  },
                symbol:{ type: 'string', optional: true  },
                valueProviderUrl: { type: 'url', optional: true  }
            },

            async handler(ctx){

                //find requested resource
                const currencyRepos = AppDataSource.getRepository(Currency)

                let entityToUpdate = await currencyRepos.findOneBy({
                    id: Number(ctx.params.id)
                })

                if(!entityToUpdate){
                    throw new Moleculer.Errors.MoleculerClientError('not found', 404)
                }

                entityToUpdate.name = ctx.params.name ?? entityToUpdate.name
                entityToUpdate.symbol = ctx.params.symbol ?? entityToUpdate.symbol
                entityToUpdate.valueProviderUrl = ctx.params.valueProviderUrl ?? entityToUpdate.valueProviderUrl

                const updatedEntity = await currencyRepos.save(entityToUpdate);

                if(updatedEntity){
                    return updatedEntity
                }

                throw new Moleculer.Errors.MoleculerClientError('internal server error', 500)
            }
        },

        list:{
            rest: "GET /list",
            
            cache: true,
            async handler(ctx){

                return await AppDataSource.manager.find(Currency);
            }
        },

        getById: {

            rest: "GET /getbyId",
            params:{
                id: { type: 'number', positive: true }
            },

            async handler(ctx){

                const currency = await AppDataSource.manager.findOneBy(Currency, {
                    id: ctx.params.id
                });

                if(!currency){
                    throw new Moleculer.Errors.MoleculerClientError('not found', 404)
                }

                return currency;
            }
        }

    }

} satisfies ServiceSchema<ServiceSettingSchema>