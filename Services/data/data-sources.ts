import { DataSource } from 'typeorm';
import { User } from '../entities/User';
import { Transaction } from '../entities/Transaction';

const AppDataSource = new DataSource({

    type: 'mysql',
    host: 'localhost',
    port: 3306,
    database: "exmoneydb",
    username: 'root',
    password: '',
    synchronize: false,
    entities: [User, Transaction],

    migrations: [],
    
    logging: true
})

export {

    AppDataSource,
    Transaction,
    User

}