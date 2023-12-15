import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {EducationComponent} from "./components/education/education.component";
import {HomeComponent} from "./components/home/home.component";
import {VerifierComponent} from "./components/verifier/verifier.component";

const routes: Routes = [
  {path: "", component: HomeComponent, pathMatch: "full"},
  {path: "trainer", component: EducationComponent},
  {path: "trainer/exercises/:exerciseId", component: VerifierComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
