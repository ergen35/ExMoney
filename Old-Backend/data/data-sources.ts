import { DataSource } from 'typeorm';
import { User } from '../entities/User';
import { Transaction } from '../entities/Transaction';
import { Currency } from '../entities/Currency';
import { createConnection as createMySqlConnection } from 'mysql';


let AppDataSource = new DataSource({

    type: 'mysql',
    host: 'localhost',
    port: 3306,
    database: "exmoneydb",
    username: 'root',
    password: '',
    synchronize: false,
    entities: [User, Transaction, Currency],

    migrations: [],
    
    logging: true
})


//Create & Init database if not exists  
function createDatabaseIfNotExists(dataSource: DataSource) {
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

AppDataSource.initialize().then(() => console.log('Database initialized'));

export {

    AppDataSource,
    Transaction,
    User,
    Currency

}