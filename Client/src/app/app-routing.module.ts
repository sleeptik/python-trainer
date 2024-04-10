import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TrainerComponent} from "./components/trainer/trainer.component";
import {assignmentResolver} from "./resolvers/assignment.resolver";
import {AssignmentsComponent} from "./components/assignments/assignments.component";
import {myAssignmentsResolver} from "./resolvers/my-assignments.resolver";
import {mySubjectsResolver} from "./resolvers/my-subjects.resolver";


const routes: Routes = [
  {
    path: "trainer",
    component: TrainerComponent,
    resolve: {assignment: assignmentResolver}
  },
  {
    path: "assignments", component: AssignmentsComponent,
    resolve: {assignments: myAssignmentsResolver, subjects: mySubjectsResolver}
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
