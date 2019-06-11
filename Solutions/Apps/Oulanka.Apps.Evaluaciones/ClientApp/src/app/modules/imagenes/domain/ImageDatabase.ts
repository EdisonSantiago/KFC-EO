import Dexie from 'dexie';

export class ImageDatabase extends Dexie {
    constructor() {
        super('ImageDatabase');
        this.version(1).stores({
            locales: 'id,orden,localId'
        });
    }
}
