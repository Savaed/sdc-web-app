import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'discountType'
})
export class DiscountTypePipe implements PipeTransform {

    transform(value: string, prefix?: string ): string {
        for (let i = 1; i < value.length; i++) {
            const char = value[i];
            if (char.toUpperCase() === char) {
                value = value.slice(0, i) + ' ' + char.toLowerCase() + value.slice(i + 1);
            }
        }

        if (prefix !== undefined) {
            const originalValue = value;
            value = value.slice(prefix.length + 1).charAt(0).toUpperCase() + originalValue.slice(prefix.length + 2);
        }
        return value;
    }
}

