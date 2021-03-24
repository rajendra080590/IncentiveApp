import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { NewFormComponent } from '../../new-form/new-form.component';
import { TypographyComponent } from '../../typography/typography.component';
import { IconsComponent } from '../../icons/icons.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { UpgradeComponent } from '../../upgrade/upgrade.component';
import { PayrollPendingComponent } from 'app/modules/payroll-pending/payroll-pending.component';
import { PayrollReviewComponent } from 'app/modules/payroll-review/payroll-review.component';


export const DefaultRoutes: Routes = [
  { path: 'home',      component: DashboardComponent },
  { path: 'Approved',      component: DashboardComponent },
  { path: 'home/:userid',     component: DashboardComponent },
  { path: 'home/:weekid/:formheaderid/:userid',     component: DashboardComponent },
  { path: 'home/:weekid/:formheaderid/:userid/:statusid',     component: DashboardComponent },
  { path: 'user-profile',   component: UserProfileComponent },
  { path: 'input-form',     component: TableListComponent },
  { path: 'input-form/:weekid',     component: TableListComponent },
  { path: 'input-form/:weekid/:formheaderid',     component: TableListComponent },
  { path: 'input-form/:weekid/:formheaderid/:userid',     component: TableListComponent },
  { path: 'input-form/:weekid/:formheaderid/:userid/:statusid',     component: TableListComponent },
  { path: 'payroll-download',      component: PayrollPendingComponent },
  { path: 'payroll-review',      component: PayrollReviewComponent },
];
