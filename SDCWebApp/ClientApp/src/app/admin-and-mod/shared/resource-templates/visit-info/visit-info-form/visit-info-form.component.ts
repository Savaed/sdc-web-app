import { Component, OnInit, Input } from '@angular/core';
import { VisitInfo, OpeningHours } from 'src/app/models/VisitInfo';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray, ControlContainer } from '@angular/forms';
import { TouchSequence } from 'selenium-webdriver';
import { ResourceService, ResourceType } from '../../../resource.service';

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


    get description() { return this.infoForm.get('description') as FormControl; }
    get maxAllowedGroupSize() { return this.infoForm.get('maxAllowedGroupSize') as FormControl; }
    get maxChildAge() { return this.infoForm.get('maxChildAge') as FormControl; }
    get maxTicketOrderInterval() { return this.infoForm.get('maxTicketOrderInterval') as FormControl; }
    get openingHours() { return this.infoForm.get('openingHours') as FormArray; }
    get sightseeingDuration() { return this.infoForm.get('sightseeingDuration') as FormControl; }


    getOpeningHour(i: number) { return this.openingHours.controls[i].get('openingHour') as FormControl; }
    getClosingHour(i: number) { return this.openingHours.controls[i].get('closingHour') as FormControl; }

    private infoForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private resourceService: ResourceService) { }

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

    addOpeningHourFormGroup(): FormGroup {
        return this.formBuilder.group({
            dayOfWeek: ['', Validators.required],
            openingHour: ['', [Validators.required, Validators.pattern(/^(?:\d|[01]\d|2[0-3]):[0-5]\d$/)]],
            closingHour: ['', [Validators.required, Validators.pattern(/^(?:\d|[01]\d|2[0-3]):[0-5]\d$/)]]
        });
    }

    private addOpeningHour() {
        this.openingHours.push(this.addOpeningHourFormGroup());
        console.log(this.openingHours);

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

        console.log('add info');
        this.resourceService.add(newVisitInfo, ResourceType.VisitInfo);
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

    private deleteHour(i: number, fromForm = true) {
        if (fromForm) {
            this.openingHours.controls.splice(i, 1);
        } else {
            this.visitInfo.openingHours.splice(i, 1);
        }
    }

    private testCRUD() {
        if (this.isForAdd) {
            this.addInfo();
        } else {
            this.edit();
        }
    }
}
