export interface ICompany {
    id: number;
    name: string;
    stockTicker: string;
    exchange: string;
    isin: string;
    websiteurl?: string | null;
}