import { Entity, PrimaryGeneratedColumn, OneToOne, OneToMany, Column } from "typeorm";
import { Transaction } from "./Transaction";

@Entity('users')
export class User{

    @PrimaryGeneratedColumn('uuid')
    id?: string

    //identity
    @Column('varchar')
    firstName?: string

    @Column('varchar')
    lastName?: string

    @Column('double', { default: 0.0 })
    birthDate?: number
    
    @Column('varchar')
    sex?: 'Male' | 'Female' = 'Male'

    @Column('varchar', { unique: true })
    phone?: string

    @Column('varchar', { unique: true })
    email?: string


    //verifications
    @Column('bool', { default: false })
    identityVerified = false

    @Column('bool', { default: false })
    emailVerified = false

    @Column('bool', { default: false })
    phoneVerified = false


    //relations
    @OneToMany(() => Transaction, t => t.transactionActor)
    transactions?: Transaction[]
}