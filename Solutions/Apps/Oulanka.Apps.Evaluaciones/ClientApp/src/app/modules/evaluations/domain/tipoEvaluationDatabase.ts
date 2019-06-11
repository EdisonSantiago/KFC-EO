import Dexie from 'dexie';
import { TipoEvaluation } from './tipoEvaluation';

export class TipoEvaluationDatabase extends Dexie {
    tipoEvaluations: Dexie.Table<TipoEvaluation, any>;

    constructor() {
        super('TipoEvaluationDatabase');
        this.version(1).stores({
            tipoEvaluations: 'id'
        });
    }
}
