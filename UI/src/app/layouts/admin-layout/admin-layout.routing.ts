import { Routes } from '@angular/router';

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

export const AdminLayoutRoutes: Routes = [
    { path: 'home',      component: DashboardComponent },
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
    { path: 'typography',     component: TypographyComponent },
    { path: 'new-form',     component: NewFormComponent },
    { path: 'icons',          component: IconsComponent },
    { path: 'maps',           component: MapsComponent },
    { path: 'notifications',  component: NotificationsComponent },
    { path: 'upgrade',        component: UpgradeComponent },
];
