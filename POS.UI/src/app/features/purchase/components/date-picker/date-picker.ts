import {ChangeDetectionStrategy, Component, output} from '@angular/core';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {type TuiDayRange} from '@taiga-ui/cdk';
import {tuiCreateDefaultDayRangePeriods, TuiInputDateRange} from '@taiga-ui/kit';

@Component({
    imports: [ReactiveFormsModule, TuiInputDateRange],
    selector: 'app-date-picker',
    templateUrl: './date-picker.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class DatePicker {
    protected readonly control = new FormControl<TuiDayRange | null>(null);
    protected readonly items = tuiCreateDefaultDayRangePeriods();
    message = output<string>();

    public get content(): string {
        const {value} = this.control;

        return value
            ? String(this.items.find((period) => period.range.daySame(value)) || '')
            : '';
    }
}
