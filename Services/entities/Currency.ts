import { Column, Entity, PrimaryGeneratedColumn } from "typeorm";

@Entity('currencies')
export class Currency{

    @PrimaryGeneratedColumn()
    id!: number

    @Column('varchar')
    name!: string

    @Column('varchar')
    symbol?: string

    @Column('text')
    valueProviderUrl?: string
}