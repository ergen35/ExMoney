import { Entity, PrimaryGeneratedColumn, ManyToOne, Column } from 'typeorm';
import { User } from './User';


@Entity('transactions')
export class Transaction{

    @PrimaryGeneratedColumn('uuid')
    id?: string

    @ManyToOne(() => User, u => u.transactions)
    transactionActor?: User

}