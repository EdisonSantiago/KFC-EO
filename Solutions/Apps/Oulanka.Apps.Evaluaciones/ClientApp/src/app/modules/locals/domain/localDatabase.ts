import Dexie from 'dexie';
import { Local } from './local';

export class LocalDatabase extends Dexie {
    locales: Dexie.Table<Local, any>;

    constructor() {
        super('LocalDatabase');
        this.version(1).stores({
            locales: 'id,codigo,tipoLocalId,cadenaId,jefeAreaId'
        });
    }
}
