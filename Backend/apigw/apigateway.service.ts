import * as ApiGateway from "moleculer-web"
import { ServiceSchema, ServiceSettingSchema } from 'moleculer'

module.exports = {
    name: "gateway",
    mixins: [ApiGateway],

    settings: {
        routes: [
            {
                path: "/api",

                aliases: {
                    
                },

                autoAliases: true
            }
        ]
    }

} satisfies ServiceSchema<ServiceSettingSchema>