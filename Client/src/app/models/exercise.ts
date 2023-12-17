import {Subject} from "./subject";

export interface Exercise {
  id: number;
  contents: string;

  rank: { id: number, name: string };
  subjects: Subject[];
}


