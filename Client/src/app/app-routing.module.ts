import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TrainerComponent} from "./components/trainer/trainer.component";
import {AssignmentsComponent} from "./components/assignments/assignments.component";
import {WelcomeComponent} from "./components/welcome/welcome.component";
import {assignmentResolver} from "./resolvers/assignment.resolver";
import {myAssignmentsResolver} from "./resolvers/my-assignments.resolver";
import {mySubjectsResolver} from "./resolvers/my-subjects.resolver";
import {DummyRedirectComponent} from "./components/dummy-redirect/dummy-redirect.component";
import {yandexLoginRedirectGuard} from "./guards/yandex-login-redirect.guard";


const routes: Routes = [
  {
    path: "trainer",
    component: TrainerComponent,
    resolve: {assignment: assignmentResolver}
  },
  {
    path: "assignments", component: AssignmentsComponent,
    resolve: {assignments: myAssignmentsResolver, subjects: mySubjectsResolver}
  },
  {
    path: "yandex-login",
    component: DummyRedirectComponent,
    canActivate: [yandexLoginRedirectGuard]
  },
  {
    path: "",
    pathMatch: "full",
    component: WelcomeComponent
  },
] as const;

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
