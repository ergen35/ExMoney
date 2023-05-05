import { Entity, PrimaryGeneratedColumn, OneToOne, OneToMany, Column } from "typeorm";
import { Transaction } from "./Transaction";

@Entity('users')
export class User{

    @PrimaryGeneratedColumn('identity')
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

    @Column("text")
    address?: string

    @Column('text')
    country?: string

    //Account infos
    
    @Column('double', { default: 0.0 })
    balance: number = 0

    @Column('double')
    creationDate?: number

    @Column('text')
    passwordHash?: string

    @Column('text')
    passwordSecret?: string



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