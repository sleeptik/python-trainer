import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {TrainerComponent} from "./components/trainer/trainer.component";
import {AssignmentsComponent} from "./components/assignments/assignments.component";
import {WelcomeComponent} from "./components/welcome/welcome.component";
import {assignmentResolver} from "./resolvers/assignment.resolver";
import {myAssignmentsResolver} from "./resolvers/my-assignments.resolver";
import {subjectsResolver} from "./resolvers/subjects.resolver";
import {DummyRedirectComponent} from "./components/dummy-redirect/dummy-redirect.component";
import {yandexLoginRedirectGuard} from "./guards/yandex-login-redirect.guard";
import {authorizedGuard} from "./guards/authorized.guard";


const routes: Routes = [
  {
    path: "assignments",
    canActivateChild: [authorizedGuard],
    children: [
      {
        path: "",
        component: AssignmentsComponent,
        resolve: {assignments: myAssignmentsResolver, subjects: subjectsResolver},
      },
      {
        path: ":assignmentId/trainer",
        component: TrainerComponent,
        resolve: {assignment: assignmentResolver}
      },
    ]
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
