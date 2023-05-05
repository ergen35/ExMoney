import { Column, Entity, ManyToOne, PrimaryGeneratedColumn } from 'typeorm';
import { User } from './User';


@Entity('transactions')
export class Transaction{

    @PrimaryGeneratedColumn('uuid')
    id?: string


    @Column('varchar')
    baseCurrency!: string

    @Column('varchar')
    changeCurrency!: string

    @Column('double')
    transactionDate?: number

    @Column('double')
    amount = 0

    @Column('double')
    rate = 0

    @Column('varchar')
    status!:  'accepted' | 'processing' | 'finished' | 'rejected'

    //relations
    @ManyToOne(() => User, u => u.transactions)
    transactionActor?: User

}