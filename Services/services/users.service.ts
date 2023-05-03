import { ServiceSchema, ServiceSettingSchema } from 'moleculer';
import { AppDataSource, User } from '../data/data-sources';

module.exports = {

    name: 'users',
    version: 1,

    settings: {

    },

    actions: {

        register:{

            // rest: 'GET /register',
            params: {
                firstName: 'string',
                lastName: 'string',
                birthDate: 'number',
                sex: 'string',
                phone: 'string',
                email: 'string'
            },

            async handler(ctx){
                
                const user = new User();
                user.firstName = ctx.params.firstName
                user.lastName = ctx.params.lastName
                user.birthDate = ctx.params.birthDate
                user.sex = ctx.params.sex
                user.phone = ctx.params.phone
                user.email = ctx.params.email

                //check existence
                const usersRepos = AppDataSource.getRepository(User);

                const u0 = await usersRepos.findOneBy({
                    email: ctx.params.email
                });

                if(!u0){
                    throw Error({
                        status: 400,
                        message: "no way"
                    }.toString())
                }
            }
        }
    },



    async started(){
        if(!AppDataSource.isInitialized){
            await AppDataSource.initialize();
        }
    }
} satisfies ServiceSchema<ServiceSettingSchema>