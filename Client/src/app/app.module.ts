import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {AppRoutingModule} from "./app-routing.module";
import {HomeComponent} from './components/home/home.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
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
import {AssignmentsComponent} from './components/assignments/assignments.component';
import {AssignmentsTableComponent} from './components/assignments-table/assignments-table.component';
import {CdkTableModule} from "@angular/cdk/table";
import {AssignmentsMenuComponent} from './components/assignments-menu/assignments-menu.component';
import {DummyRedirectComponent} from './components/dummy-redirect/dummy-redirect.component';
import {TrainerLoadingOverlayComponent} from './components/trainer-loading-overlay/trainer-loading-overlay.component';
import {MatProgressBar} from "@angular/material/progress-bar";
import {MathjaxModule} from "mathjax-angular";

const MathJaxConfig = {
  "config": {
    "loader": {
      "load": ["output/svg", "[tex]/require", "[tex]/ams"]
    },
    "tex": {
      "inlineMath": [["\\(", "\\)"]],
      "packages": ["base", "require", "ams"]
    },
    "svg": {"fontCache": "global"}
  },
  "src": "https://cdn.jsdelivr.net/npm/mathjax@3.2.2/es5/startup.js"
};

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    WelcomeComponent,
    TrainerLayoutComponent,
    TrainerInformationComponent,
    TrainerEditorComponent,
    TrainerComponent,
    TrainerMenuComponent,
    TrainerResultComponent,
    TrainerOutputComponent,
    AssignmentsComponent,
    AssignmentsTableComponent,
    AssignmentsMenuComponent,
    DummyRedirectComponent,
    TrainerLoadingOverlayComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MonacoEditorModule.forRoot(),
    MathjaxModule.forRoot(MathJaxConfig),
    CdkAccordionModule,
    CdkTableModule,
    MatProgressBar
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
