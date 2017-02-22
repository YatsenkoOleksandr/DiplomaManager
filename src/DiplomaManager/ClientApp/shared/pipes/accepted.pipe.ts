import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'accepted'
})
export class AcceptedPipe implements PipeTransform {
    transform(value: boolean, args?: any[]) {
        return value ? "Да" : "Нет";
    }
}