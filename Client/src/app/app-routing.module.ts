import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TrainerComponent} from "./components/trainer/trainer.component";
import {assignmentResolver} from "./resolvers/assignment.resolver";


const routes: Routes = [
  {path: "trainer", component: TrainerComponent, resolve: {assignment: assignmentResolver}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
