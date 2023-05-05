import { ServiceSchema, ServiceSettingSchema } from 'moleculer';
import { AppDataSource, User } from '../data/data-sources';
import { Errors as MolErrors} from 'moleculer'
import { hashSync, genSaltSync } from 'bcrypt'
import { AppConfig } from '../config/config';

module.exports = {

    name: 'users',
    version: 1,

    settings: {

    },

    actions: {

        register:{

            rest: 'POST /register',
           
            params: {
                firstName: { type: "string", min: 2, max: 255 },
                lastName:  { type: "string", min: 2, max: 255 },
                birthDate: { type: "number" },
                sex: { type: 'string', enum: ['Male', 'Female']},
                phone: { type: "string", min: 8, max: 30, optional: true },
                email: { type: "email" },
                password: { type: "string", min: 6 }
            },

            async handler(ctx){
                
                let user = new User();

                user.firstName = ctx.params.firstName
                user.lastName = ctx.params.lastName
                user.birthDate = ctx.params.birthDate
                user.sex = ctx.params.sex
                user.phone = ctx.params.phone ?? ""
                user.email = ctx.params.email
                

                //check existence
                const usersRepos = AppDataSource.getRepository(User);

                const u0 = await usersRepos.findOneBy({
                    email: ctx.params.email
                });

                if(u0){
                    throw new MolErrors.MoleculerClientError('This email is already used', 400)
                }

                if(user.phone){
                   const u1 = await usersRepos.findOneBy({
                        phone: ctx.params.phone
                   }) 

                   if(u1){
                        throw new MolErrors.MoleculerClientError('This phone is already used', 400)
                   }
                }

                //All okay
                //define creation date
                user.creationDate = Date.now()

                //Hash password
                const salt = genSaltSync(10) 
                user.passwordSecret = salt
                user.passwordHash = hashSync(ctx.params.password, salt)
                
                //save user into database
                user = await usersRepos.save(user);

                if(user){
                    return structuredClone(user);
                }
                else{
                    return new MolErrors.MoleculerClientError("Une erreur s'est produite", 500)
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