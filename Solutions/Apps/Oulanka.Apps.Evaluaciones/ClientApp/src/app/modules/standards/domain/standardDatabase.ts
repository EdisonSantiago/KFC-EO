import Dexie from 'dexie';

export class StandardDatabase extends Dexie {

    constructor() {
        super('StandardDatabase');
        this.version(1).stores({
            standards: 'id,codigo,estandarPadreId,grupoEstandarId,nombre',
            grupoStandards: 'id,codigo,nombre'
        });
    }
}
