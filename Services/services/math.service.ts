import { ServiceSchema, ServiceSettingSchema } from "moleculer";


module.exports = {
    name: "math",
    version: 2,

    actions:{
        add: {

            rest: "GET /add",
            handler(ctx){
                if(ctx.params.a && ctx.params.b){
                    const a = Number(ctx.params.a);
                    const b = Number(ctx.params.b);

                    if(Number.isNaN(a) || Number.isNaN(b))
                    {
                        throw Error('Incorrect parameters format')
                    }

                    return   Number(ctx.params.a) + Number(ctx.params.b)
                }
                
                throw Error('Bad request')
            }
        },

        sub(ctx){
            if(ctx.params.a && ctx.params.b){
                const a = Number(ctx.params.a);
                const b = Number(ctx.params.b);

                if(Number.isNaN(a) || Number.isNaN(b))
                {
                    throw Error('Incorrect parameters format')
                }

                return   Number(ctx.params.a) - Number(ctx.params.b)
            }
            
            throw Error('Bad request')
        }
    }
} satisfies ServiceSchema<ServiceSettingSchema>