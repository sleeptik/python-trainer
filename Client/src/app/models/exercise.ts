import {Subject} from "./subject";

export interface Exercise {
  id: number;
  details: string;

  rank: { id: number, name: string };
  subjects: Subject[];
}


