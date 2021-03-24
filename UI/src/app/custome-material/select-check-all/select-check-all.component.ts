import { Component, Input, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';

@Component({
  selector: 'app-select-check-all',
  templateUrl: "./select-check-all.component.html",
  styleUrls: ['./select-check-all.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SelectCheckAllComponent {
  @Input() model: FormControl;
  @Input() values = [];
  @Input() text = 'Select All'; 

  isChecked(): boolean {
    debugger;
    return this.model.value && this.values.length
      && this.model.value.length === this.values.length;
  }

  isIndeterminate(): boolean {
    return this.model.value && this.values.length && this.model.value.length
      && this.model.value.length < this.values.length;
  }

  toggleSelection(change: MatCheckboxChange): void {
    debugger;
    if (change.checked) {
      this.model.setValue(this.values);
    } else {
      this.model.setValue([]);
    }
  }
}
