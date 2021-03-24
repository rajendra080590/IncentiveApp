import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { DefaultComponent } from './default.component';
import { MatSidenavModule} from '@angular/material/sidenav';
import { MatDividerModule} from '@angular/material/divider';
import { MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatRippleModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSelectModule} from '@angular/material/select';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import { MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {MatSliderModule} from '@angular/material/slider';
import { MatMenuModule } from '@angular/material/menu';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { DefaultRoutes } from './default.routing';
import { DashboardComponent } from 'app/dashboard/dashboard.component';
import { TableListComponent } from 'app/table-list/table-list.component';
import { PayrollPendingComponent } from 'app/modules/payroll-pending/payroll-pending.component';
import { PayrollReviewComponent } from 'app/modules/payroll-review/payroll-review.component';
import { AudittraildialogComponent } from 'app/dashboard/audittraildialog/audittraildialog.component';
import { DownloadComponent } from 'app/modules/download/download.component';

import { CustomMaterialModule } from 'app/custome-material/custom-material.module';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
 
import { FilterPipe } from '../../Pipes/filter.pipe';
import {SpinnerService} from '../../services/spinner.service';
import { MatIconModule } from '@angular/material/icon';
import { TwoDigitDecimaNumberDirective } from '../../directives/two-digit-decima-number.directive';
import { FileInputAccessorDirective } from '../../directives/file-input-accessor.directive';
import { CurrencyInputDirective } from '../../directives/currency-input.directive';

import { MasterService }  from '../../services/master.service';
import { InputformService } from '../../services/inputform.service';
import { DashboardService } from '../../services/dashboard.service';
import { SidenavService } from '../../services/sidenav.service'
import { PayrollService } from 'app/services/payroll.service';
import { UploadDownloadService } from 'app/services/upload-download.service';

import {DataTablesModule} from 'angular-datatables';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { TitleCasePipe } from '../../Pipes/title-case.pipe';

import { NgxCurrencyModule } from "ngx-currency";
import { InputFileConfig, InputFileModule } from 'ngx-input-file';
import { NgxTextEditorModule } from 'ngx-text-editor';
import { MaterialFileInputModule } from 'ngx-material-file-input';

const config: InputFileConfig = {
  fileAccept: '.pdf,.doc,.word,.txt,.xlsx',
  fileLimit: 4
};

@NgModule({
  declarations: [
    DefaultComponent,
    DashboardComponent,
    TableListComponent,
    PayrollPendingComponent,
    PayrollReviewComponent,
    AudittraildialogComponent,
    FilterPipe,
    TwoDigitDecimaNumberDirective,
    FileInputAccessorDirective,
    CurrencyInputDirective,
    DownloadComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(DefaultRoutes),
    SharedModule,
    MatSidenavModule,
    MatMenuModule,
    MatDividerModule,
    MatCardModule,
    MatPaginatorModule,
    MatTableModule,
    DataTablesModule,
    MatButtonModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    MatCheckboxModule,
    MatPaginatorModule,
    MatTableModule,
    MatIconModule,
    DataTablesModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxCurrencyModule,
    InputFileModule.forRoot(config),
    NgxTextEditorModule,
    MaterialFileInputModule,
    NgMultiSelectDropDownModule.forRoot()
    //CustomMaterialModule
    //NgxMatFileInputModule
  ],
  providers: [
    //SpinnerService,
    MasterService, InputformService, DatePipe, DashboardService, SidenavService, TitleCasePipe, CurrencyPipe,
    PayrollService, UploadDownloadService
  ]
})
export class DefaultModule { }
