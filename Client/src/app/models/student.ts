import {Subject} from "./subject";
import {Rank} from "./rank";

export interface Student {
  userId: number;
  score: number;

  currentRankId: number;
  currentRank: Rank;
}


