import Dexie from 'dexie';
import { Evaluation } from './evaluation';

export class EvaluationDatabase extends Dexie {
    evaluations: Dexie.Table<Evaluation, any>;

    constructor() {
        super('EvaluationDatabase');
        this.version(1).stores({
            evaluations: 'id,localId,cadenaId,tipoEvaluacionId,tipoLocalId'
        });
    }
}
