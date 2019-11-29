import { Component, OnInit, Input } from '@angular/core';
import { VisitInfo, OpeningHours } from 'src/app/models/VisitInfo';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray, ValidatorFn, ValidationErrors } from '@angular/forms';
import { ResourceService, ResourceType } from '../../../resource.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-visit-info-form',
    templateUrl: './visit-info-form.component.html',
    styleUrls: ['./visit-info-form.component.scss']
})
export class VisitInfoFormComponent implements OnInit {
    @Input() public isForAdd = false;
    @Input() public visitInfo: VisitInfo = {
        description: '',
        maxAllowedGroupSize: 0,
        maxChildAge: 0,
        maxTicketOrderInterval: 0,
        openingHours: null,
        sightseeingDuration: 0,
    };

    public infoForm: FormGroup;

    public get description() { return this.infoForm.get('description') as FormControl; }
    public get maxAllowedGroupSize() { return this.infoForm.get('maxAllowedGroupSize') as FormControl; }
    public get maxChildAge() { return this.infoForm.get('maxChildAge') as FormControl; }
    public get maxTicketOrderInterval() { return this.infoForm.get('maxTicketOrderInterval') as FormControl; }
    public get openingHours() { return this.infoForm.get('openingHours') as FormArray; }
    public get sightseeingDuration() { return this.infoForm.get('sightseeingDuration') as FormControl; }

    constructor(private formBuilder: FormBuilder, private resourceService: ResourceService, private toast: ToastrService) { }

    ngOnInit() {
        this.infoForm = this.formBuilder.group({
            description: [this.visitInfo.description, [Validators.required, Validators.maxLength(256)]],
            maxAllowedGroupSize: [this.visitInfo.maxAllowedGroupSize, Validators.required],
            maxChildAge: [this.visitInfo.maxChildAge, Validators.required],
            maxTicketOrderInterval: [this.visitInfo.maxTicketOrderInterval, Validators.required],
            sightseeingDuration: [this.visitInfo.sightseeingDuration, Validators.required],
            openingHours: this.formBuilder.array([
                this.addOpeningHourFormGroup()
            ])
        });
    }

    private addOpeningHourFormGroup(): FormGroup {
        return this.formBuilder.group({
            dayOfWeek: ['', Validators.required],
            openingHour: ['', [Validators.required, Validators.pattern(/^(?:\d|[01]\d|2[0-3]):[0-5]\d$/)]],
            closingHour: ['', [Validators.required, Validators.pattern(/^(?:\d|[01]\d|2[0-3]):[0-5]\d$/)]]
        }, { validators: this.validHours });
    }


    public validHours: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
        const openingHourString = control.get('openingHour').value as string;
        const closingHourString = control.get('closingHour').value as string;

        const openingHour = { h: +openingHourString.split(':')[0], m: +openingHourString.split(':')[1] };
        const closingHour = { h: +closingHourString.split(':')[0], m: +closingHourString.split(':')[1] };

        if (openingHour.h > closingHour.h) {
            return { invalidHour: 'opening hour must be before closing hour' };
        } else if (openingHour.h === closingHour.h && openingHour.m > closingHour.m) {
            return { invalidHour: 'opening hour must be before closing hour' };
        }

        return null;
    }

    public addOpeningHour() {
        this.openingHours.push(this.addOpeningHourFormGroup());
    }

    private addInfo() {
        const newVisitInfo: VisitInfo = {
            description: this.description.value,
            maxAllowedGroupSize: this.maxAllowedGroupSize.value,
            maxChildAge: this.maxChildAge.value,
            maxTicketOrderInterval: this.maxTicketOrderInterval.value,
            sightseeingDuration: this.sightseeingDuration.value,
            openingHours: this.getAllOpeningHours()
        };

        this.resourceService.add(newVisitInfo, ResourceType.VisitInfo);
        this.toast.info('New visit info has been added.');
    }

    private isDifferent(oldInfo: VisitInfo, newInfo: VisitInfo): boolean {
        return (oldInfo.sightseeingDuration !== newInfo.sightseeingDuration
            || oldInfo.description !== newInfo.description
            || oldInfo.maxAllowedGroupSize !== newInfo.maxAllowedGroupSize
            || oldInfo.maxChildAge !== newInfo.maxChildAge
            || oldInfo.maxTicketOrderInterval !== newInfo.maxTicketOrderInterval);
    }

    private edit() {
        const updatedInfo: VisitInfo = {
            id: this.visitInfo.id,
            description: this.description.value,
            maxAllowedGroupSize: this.maxAllowedGroupSize.value,
            maxChildAge: this.maxChildAge.value,
            maxTicketOrderInterval: this.maxTicketOrderInterval.value,
            sightseeingDuration: this.sightseeingDuration.value,
            openingHours: this.getAllOpeningHours().concat(this.visitInfo.openingHours)
        };

        if (this.isDifferent(updatedInfo, this.visitInfo)) {
            this.resourceService.edit(updatedInfo);
            this.toast.info('Visit info has been edited.');
        }
    }

    private getAllOpeningHours(): OpeningHours[] {
        const tmpHours = new Array<OpeningHours>();

        this.openingHours.controls.forEach(control => {
            if (control.value.dayOfWeek !== '' && control.value.openingHour !== '' && control.value.closingHour !== '') {
                const hour: OpeningHours = {
                    dayOfWeek: control.value.dayOfWeek,
                    openingHour: control.value.openingHour,
                    closingHour: control.value.closingHour,
                };

                tmpHours.push(hour);
            }
        });

        return tmpHours;
    }

    public deleteHour(i: number, fromForm = true) {
        if (fromForm) {
            this.openingHours.controls.splice(i, 1);
        } else {
            this.visitInfo.openingHours.splice(i, 1);
        }
    }

    public testCRUD() {
        if (this.isForAdd) {
            this.addInfo();
        } else {
            this.edit();
        }
    }
}
