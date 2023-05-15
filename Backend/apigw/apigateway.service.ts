import * as ApiGateway from "moleculer-web"
import { default as ApiGatewayOptions } from "moleculer-web"
import Moleculer, { ServiceSchema, ServiceSettingSchema } from 'moleculer'

//TODO: npm install jsonwebtoken


module.exports = {
    name: "api-gateway",
    mixins: [ApiGateway],

    settings: {
        routes: [
            {
                // path prefix
                path: "/api",

                aliases: {},

                //Enable authorization
                authorize: true,

                //Enable authentication
                authenticate: true,

                //Enable auto aliases feature
                autoAliases: true,
            }
        ],

        
    },
    
    methods: {

        authenticate(ctx: Moleculer.Context, route: ApiGateway.Route, 
                     req: ApiGateway.IncomingRequest, res: ApiGateway.GatewayResponse)
        {
            //extract authorizeHeader from headers
            let authHeader = req.headers.authorization;

            if(!authHeader || !authHeader.startsWith("Bearer")){
                return Promise.reject(new ApiGatewayOptions.Errors.UnAuthorizedError(ApiGatewayOptions.Errors.ERR_NO_TOKEN, req))
            }

            const accessToken = authHeader.slice(7);
            
            //TODO: use access token to fetch ID token
            //TODO: verify then decode ID token
            //TODO: extract user infos { username, id, email }
            //TODO: set user infos into "ctx.meta.user"
        },

        authorize(ctx: Moleculer.Context, route: ApiGateway.Route, 
                  req: ApiGateway.IncomingRequest, res: ApiGateway.GatewayResponse)
        {
            //extract authorizeHeader from headers
            let authHeader = req.headers.authorization;

            if(!authHeader || !authHeader.startsWith("Bearer")){
                return Promise.reject(new ApiGatewayOptions.Errors.UnAuthorizedError(ApiGatewayOptions.Errors.ERR_NO_TOKEN, req))
            }

            const accessToken = authHeader.slice(7);

            //TODO: verify then decode access token
            //TODO: extract roles
            //TODO: set roles into "ctx.meta.userRoles"
        },
    }

} satisfies ServiceSchema<ServiceSettingSchema>