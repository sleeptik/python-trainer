import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {EducationComponent} from './components/education/education.component';
import {AppRoutingModule} from "./app-routing.module";
import {VerifierComponent} from './components/verifier/verifier.component';
import {HomeComponent} from './components/home/home.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import {EducationExercisesComponent} from "./components/education-exercises/education-exercises.component";
import {EducationControlComponent} from "./components/education-control/education-control.component";
import {EducationRatingComponent} from './components/education-rating/education-rating.component';
import {EducationHistoryBarComponent} from './components/education-history-bar/education-history-bar.component';
import {MatPaginatorModule} from "@angular/material/paginator";
import {CommonModule} from "@angular/common";
import {WelcomeComponent} from './components/welcome/welcome.component';
import {TrainerLayoutComponent} from './components/trainer-layout/trainer-layout.component';
import {TrainerInformationComponent} from './components/trainer-information/trainer-information.component';
import {TrainerEditorComponent} from './components/trainer-editor/trainer-editor.component';
import {TrainerComponent} from './components/trainer/trainer.component';
import {MonacoEditorModule} from "ngx-monaco-editor-v2";
import {TrainerMenuComponent} from './components/trainer-menu/trainer-menu.component';
import {CdkAccordionModule} from "@angular/cdk/accordion";
import {TrainerResultComponent} from './components/trainer-result/trainer-result.component';
import {TrainerOutputComponent} from './components/trainer-output/trainer-output.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    EducationComponent,
    EducationExercisesComponent,
    EducationControlComponent,
    EducationRatingComponent,
    EducationHistoryBarComponent,
    VerifierComponent,
    WelcomeComponent,
    TrainerLayoutComponent,
    TrainerInformationComponent,
    TrainerEditorComponent,
    TrainerComponent,
    TrainerMenuComponent,
    TrainerResultComponent,
    TrainerOutputComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MonacoEditorModule.forRoot(),
    CdkAccordionModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
