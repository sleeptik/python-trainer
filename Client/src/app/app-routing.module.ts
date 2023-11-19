import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {EducationComponent} from "./components/education/education.component";

const routes: Routes = [
  {path: "", component: EducationComponent, pathMatch: "full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
