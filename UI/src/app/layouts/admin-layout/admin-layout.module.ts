import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { PayrollPendingComponent } from '../../modules/payroll-pending/payroll-pending.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { NewFormComponent } from '../../new-form/new-form.component';
import { TypographyComponent } from '../../typography/typography.component';
import { IconsComponent } from '../../icons/icons.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { AudittraildialogComponent } from '../../dashboard/audittraildialog/audittraildialog.component';
import { UpgradeComponent } from '../../upgrade/upgrade.component';
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
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatSliderModule} from '@angular/material/slider';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';

import { FilterPipe } from '../../Pipes/filter.pipe';
import {SpinnerService} from '../../services/spinner.service';
import { MatIconModule } from '@angular/material/icon';
import { TwoDigitDecimaNumberDirective } from '../../directives/two-digit-decima-number.directive';
import { FileInputAccessorDirective } from '../../directives/file-input-accessor.directive';

import { MasterService }  from '../../services/master.service';
import { InputformService } from '../../services/inputform.service';
import { DashboardService } from '../../services/dashboard.service';
import { SidenavService } from '../../services/sidenav.service'

import {DataTablesModule} from 'angular-datatables';
import { DatePipe } from '@angular/common';
import { TitleCasePipe } from '../../Pipes/title-case.pipe';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
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
    MatNativeDateModule
  ],
  declarations: [
    DashboardComponent,
    PayrollPendingComponent,
    UserProfileComponent,
    TableListComponent,
    TypographyComponent,
    NewFormComponent,
    IconsComponent,
    MapsComponent,
    NotificationsComponent,
    AudittraildialogComponent,
    UpgradeComponent,
    FilterPipe,
    TwoDigitDecimaNumberDirective,
    FileInputAccessorDirective
  ],
  providers: [
    //SpinnerService,
    MasterService, InputformService, DatePipe, DashboardService, SidenavService, TitleCasePipe
  ],
})

export class AdminLayoutModule {}
