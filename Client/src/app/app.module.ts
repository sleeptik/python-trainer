import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {EducationComponent} from './components/education/education.component';
import {EducationExerciseComponent} from './components/education-exercise/education-exercise.component';
import {EducationListComponent} from './components/education-list/education-list.component';

@NgModule({
  declarations: [
    AppComponent,
    EducationComponent,
    EducationExerciseComponent,
    EducationListComponent
  ],
  imports: [
    BrowserModule, HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
