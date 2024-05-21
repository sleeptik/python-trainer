import {Subject} from "./subject";
import {Rank} from "./rank";

export interface Exercise {
  id: number;
  details: string;

  rank: Rank;
  subjects: Subject[];
}


