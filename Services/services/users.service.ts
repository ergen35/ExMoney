import { ServiceSchema, ServiceSettingSchema } from 'moleculer';
import { AppDataSource, User } from '../data/data-sources';
import { Errors as MolErrors} from 'moleculer'
import { hashSync, genSaltSync } from 'bcrypt'

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
                password: { type: "string", min: 6 },
                country:  { type: "string" }
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
        },

        login: {
            
            rest: "POST /login",
            params: {
                username: { type: 'string', min: 3 },
                password: { type: 'string', min: 6 },
                isEmail: { type: "boolean", default: true }
            },

            handler(ctx){

                let user = null
                const usersRepos = AppDataSource.getRepository(User);

                if(ctx.params.isEmail === true){

                    user = usersRepos.findOneBy({
                        email: ctx.params.username
                    });

                    if(!user){
                        throw new MolErrors.MoleculerClientError('user not found', 404)
                    }
                }
                else
                {
                    user = usersRepos.findOneBy({
                        phone: ctx.params.username
                    })
                    
                    if(!user){
                        throw new MolErrors.MoleculerClientError('user not found', 404)
                    } 
                }
                
            
                //TODO: user exists: log user | return login token
                return {
                    token: "lkedzekldeckldceazckdhjedaz"
                }
            }

        },

        list: {
            
            rest: 'GET /list',

            async handler(ctx){

                const usersRepos = AppDataSource.getRepository(User);

                const users = await usersRepos.find();
    
                return users;
            },
        },

        getById: {

            rest: "GET /getById",
            params: {
                id: 'string'
            },

            async handler(ctx){

                const user = await AppDataSource.manager.findOneBy(User, {
                    id: ctx.params.id
                });

                if(!user){
                    throw new MolErrors.MoleculerClientError('not found', 404);
                }

                return user;
            }

        },

        getByEmail: {

            rest: "GET /getByEmail",
            params: {
                email: 'string'
            },

            async handler(ctx){

                const user = await AppDataSource.manager.findOneBy(User, {
                    email: ctx.params.email
                });

                if(!user){
                    throw new MolErrors.MoleculerClientError('not found', 404);
                }

                return user;
            }

        },
        
        update: {

            rest: 'PUT /update',
            params: {
                id: 'string',

                firstName: { type: 'string', optional: true },
                lastName: { type: 'string', optional: true },
                address: { type: 'string', optional: true },
                country: { type: 'string', optional: true },
                sex: { type: 'string', enum: ['Male', 'Female'] },
                birthDate: { type: 'number', positive: true, optional: true }
            },

            async handler(ctx){

                const usersRepos = AppDataSource.getRepository(User);

                let user = await usersRepos.findOneBy({
                    id: ctx.params.id
                })

                if(!user){
                    throw new MolErrors.MoleculerClientError('user not found', 404)
                }

                user.address = ctx.params.address
                user.firstName = ctx.params.firstName
                user.lastName = ctx.params.lastName
                user.country = ctx.params.country
                user.sex = ctx.params.sex
                user.birthDate = ctx.params.birthDate

                
                user = await usersRepos.save(user);

                if(user){
                    return user;
                }

                throw new MolErrors.MoleculerClientError('internal server error', 500)
            }
        }

        //TODO: update email action
        //TODO: update phone
        //TODO: update password
    },

    async started(){
        if(!AppDataSource.isInitialized){
            await AppDataSource.initialize();
        }
    }
} satisfies ServiceSchema<ServiceSettingSchema>