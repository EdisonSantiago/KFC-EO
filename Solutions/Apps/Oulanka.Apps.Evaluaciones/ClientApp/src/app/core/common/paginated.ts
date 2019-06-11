export class Paginated {
    public page = 0;
    public pagesCount = 0;
    public totalCount = 0;

    constructor(page: number, pagesCount: number, totalCount: number) {
        this.page = page;
        this.pagesCount = pagesCount;
        this.totalCount = totalCount;
    }

    range(): Array<any> {
        if (!this.pagesCount) { return []; }
        const step = 2;
        const doubleStep = step * 2;
        const start = Math.max(0, this.page - step);
        let end = start + 1 + doubleStep;
        if (end > this.pagesCount) { end = this.pagesCount; }

        const ret = [];
        for (let i = start; i !== end; ++i) {
            ret.push(i);
        }

        return ret;
    }

    pagePlus(count: number): number {
        return + this.page + count;
    }

    search(i): void {
        this.page = i;
    }
}
