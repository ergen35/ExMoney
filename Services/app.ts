import 'reflect-metadata';

import { ServiceBroker } from 'moleculer';
import { createConnection as createMySqlConnection } from 'mysql';
import { DataSource } from 'typeorm';
import { AppDataSource } from './data/data-sources';

const brokerZero = new ServiceBroker({
    namespace: "dev",
    transporter: "NATS",

    logger: true,
    hotReload: false,
    logLevel: "info",
    metadata: {

    },

    started()
    {

        //Create & Init database if not exists  
        function createDatabaseIfNotExists(dataSource: DataSource) {
            //FIXME: too multiple
            var connection = createMySqlConnection({
                host: 'localhost',
                user: 'root',
                password: ''
            })

            connection.connect((err) => {
                if (err) throw err;
            })

            connection.query("CREATE DATABASE IF NOT EXISTS " + dataSource.options.database + ";", (err2, result) => {
                if (err2) throw err2;
                else console.log("Database created");
            })
        }

        //create if not exist
        createDatabaseIfNotExists(AppDataSource);

        //init database connection
        AppDataSource.initialize()
    }
});



//create api gateway
brokerZero.loadService('./apigw/apigateway.service');

//load services
brokerZero.loadServices('./services', "**.service.ts");

//start brokerZero
brokerZero.start();
