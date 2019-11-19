import { Component, OnInit, Input } from '@angular/core';
import { TicketTariff } from 'src/app/models/TicketTariff';
import { FormControl, FormArray, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { VisitInfo, OpeningHours } from 'src/app/models/VisitInfo';
import { ResourceType, ResourceService } from '../../../resource.service';
import { TicketTariffService } from 'src/app/services/ticket-tariff.service';

@Component({
    selector: 'app-ticket-tariff-form',
    templateUrl: './ticket-tariff-form.component.html',
    styleUrls: ['./ticket-tariff-form.component.scss']
})
export class TicketTariffFormComponent implements OnInit {
    private tariffForm: FormGroup;
    private visitTariffs = new Array<{ visitTariffName: string, visitTariffId: string }>();

    @Input() public tariff: TicketTariff = { visitTariffId: '', defaultPrice: 1, features: [], isPerHour: false, isPerPerson: false, overview: '', title: '' };
    @Input() public isForAdd = false;

    constructor(private formBuilder: FormBuilder, private resourceService: ResourceService, private ticketTariffService: TicketTariffService) { }

    get visitTariffName() { return this.tariffForm.get('visitTariffName') as FormControl; }
    get title() { return this.tariffForm.get('title') as FormControl; }
    get overview() { return this.tariffForm.get('overview') as FormControl; }
    get isPerHour() { return this.tariffForm.get('isPerHour') as FormControl; }
    get isPerPerson() { return this.tariffForm.get('isPerPerson') as FormControl; }
    get defaultPrice() { return this.tariffForm.get('defaultPrice') as FormControl; }
    get features() { return this.tariffForm.get('features') as FormArray; }

    ngOnInit() {
        this.ticketTariffService.getAllVisitTariffs()
            .subscribe(vt => vt.forEach((visitTariff) => {
                this.visitTariffs.push({ visitTariffName: visitTariff.name, visitTariffId: visitTariff.id });
            }));

        console.log(this.visitTariffs);


        this.tariffForm = this.formBuilder.group({
            visitTariffName: [this.getVisitTariffName(this.tariff.visitTariffId), this.isForAdd ? Validators.required : null ],
            title: [this.tariff.title, [Validators.required, Validators.maxLength(50)]],
            overview: [this.tariff.overview, [Validators.required, Validators.maxLength(50)]],
            isPerHour: [this.tariff.isPerHour, [Validators.required]],
            isPerPerson: [this.tariff.isPerPerson, [Validators.required]],
            defaultPrice: [this.tariff.defaultPrice, [Validators.required]],
            features: this.formBuilder.array([
                this.addFeatureFormGroup()
            ])
        });
    }

    private getVisitTariffName(id: string): string {
        const searchedVisitTariff = this.visitTariffs.find(x => x.visitTariffId === id);

        if (searchedVisitTariff !== undefined) {
            return searchedVisitTariff.visitTariffName;
        }

        return '';
    }

    addFeatureFormGroup(): FormGroup {
        return this.formBuilder.group({
            feature: ['', [Validators.required, Validators.maxLength(40)]]
        });
    }

    private addFeature() {
        this.features.push(this.addFeatureFormGroup());
        console.log(this.features.controls);
        
    }

    private add() {
        const newTariff: TicketTariff = {
            visitTariffId: this.visitTariffs.find(x => x.visitTariffName === this.visitTariffName.value).visitTariffId,
            defaultPrice: this.defaultPrice.value,
            isPerHour: this.isPerHour.value,
            isPerPerson: this.isPerPerson.value,
            overview: this.overview.value,
            title: this.title.value,
            features: this.getFeatures()
        };

        console.log('add ticket tariff');
        this.resourceService.add(newTariff, ResourceType.TicketTariff);
    }

    private isDifferent(oldTariff: TicketTariff, newTariff: TicketTariff): boolean {
        return (oldTariff.title !== newTariff.title
            || oldTariff.defaultPrice !== newTariff.defaultPrice
            || oldTariff.isPerHour !== newTariff.isPerHour
            || oldTariff.isPerPerson !== newTariff.isPerPerson
            || oldTariff.overview !== newTariff.overview);
    }

    private edit() {
        const updatedTariff: TicketTariff = {
            visitTariffId: this.tariff.visitTariffId,
            id: this.tariff.id,
            defaultPrice: this.defaultPrice.value,
            isPerHour: this.isPerHour.value,
            isPerPerson: this.isPerPerson.value,
            overview: this.overview.value,
            title: this.title.value,
            features: this.getFeatures().concat(this.tariff.features)
        };

        if (this.isDifferent(updatedTariff, this.tariff)) {
            this.resourceService.edit(updatedTariff);
        }
    }

    private getFeatures(): string[] {
        const tmpFeatures = new Array<string>();

        this.features.controls.forEach(control => {
            if (control.value.feature !== '') {
                tmpFeatures.push(control.value.feature);
            }
        });

        return tmpFeatures;
    }

    private deleteFeature(i: number, fromForm = true) {
        if (fromForm) {
            this.features.controls.splice(i, 1);
        } else {
            this.tariff.features.splice(i, 1);
        }
    }

    private testCRUD() {
        if (this.isForAdd) {
            this.add();
        } else {
            this.edit();
        }
    }
}

