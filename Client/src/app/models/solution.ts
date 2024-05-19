export interface Solution {
    id: number;
    code: string;
    submitedAt: Date;
    verifiedAt: Date | undefined;
    review: {id: number, isCorrect: boolean};
}


