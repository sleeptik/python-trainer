export interface Exercise {
  contents: string,
  difficulty: { id: number, name: string }
  subjects: { id: number, name: string }[]
}
