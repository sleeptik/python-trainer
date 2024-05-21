import {Review} from "./review";

export interface Solution {
  id: number;
  code: string;
  submitedAt: Date;
  verifiedAt: Date | undefined;
  review: Review;
}


