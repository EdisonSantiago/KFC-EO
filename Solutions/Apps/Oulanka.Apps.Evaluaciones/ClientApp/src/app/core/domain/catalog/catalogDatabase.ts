import Dexie from 'dexie';

export class CatalogDatabase extends Dexie {

    constructor() {
        super('CatalogDatabase');
        this.version(1).stores({
            posiciones: 'id,idCadena',
            tiposVisita: 'id',
            partesDia: 'id'
        });
    }
}
