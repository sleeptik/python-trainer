import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {EducationComponent} from "./components/education/education.component";
import {HomeComponent} from "./components/home/home.component";
import {ExerciseComponent} from "./components/exercise/exercise.component";
import {exerciseResolver} from "./resolvers/exercise-resolver";

const routes: Routes = [
  {path: "trainer/exercises/:exerciseId", component: ExerciseComponent, resolve: {exercise: exerciseResolver}},
  {path: "trainer", component: EducationComponent},
  {path: "", component: HomeComponent, pathMatch: "full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
