import {Suggestion} from "./suggestion";

export interface Review {
  id: number;
  isCorrect: boolean;
  suggestions: Suggestion[] | null;
}
