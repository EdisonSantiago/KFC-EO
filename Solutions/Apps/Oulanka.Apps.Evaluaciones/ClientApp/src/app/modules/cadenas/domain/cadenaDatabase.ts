import Dexie from 'dexie';
import { Cadena } from './cadena';

export class CadenaDatabase extends Dexie {
    cadenas: Dexie.Table<Cadena, any>;

    constructor() {
        super('CadenaDatabase');
        this.version(1).stores({
            cadenas: 'id'
        });
    }
}
