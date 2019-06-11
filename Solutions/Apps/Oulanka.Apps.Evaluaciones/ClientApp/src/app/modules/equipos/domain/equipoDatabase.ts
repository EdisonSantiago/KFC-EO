import Dexie from 'dexie';

export class EquipoDatabase extends Dexie {
    constructor() {
        super('EquipoDatabase');
        this.version(1).stores({
          tree: 'id,parentId',
          equiposlocales:'id,utilidad,control,cantidad,nombre,idlocal'
        });
    }
}
