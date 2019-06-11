import Dexie from 'dexie';

export class RespuestaDatabase extends Dexie {
    constructor() {
        super('RespuestaDatabase');
        this.version(1).stores({
            respuestas: 'id,evaluacionId,estandarId,[estandarId+evaluacionId]',
            comentarios: 'id,respuestaId'
        });
    }
}
