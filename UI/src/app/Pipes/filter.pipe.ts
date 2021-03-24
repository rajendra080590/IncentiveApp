import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any[], filterString: number, propName: string): any[] {

    const resultArray = [];
    if (value)
    {
      if (value.length === 0) {
        return value;
      }
      console.log(filterString);
      for (const item of value) {
        if (item[propName] === filterString) {
          resultArray.push(item);
        }
      }
      return resultArray;
    }
  }
}
