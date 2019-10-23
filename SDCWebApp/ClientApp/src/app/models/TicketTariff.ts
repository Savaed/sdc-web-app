import { BaseResponseDataType } from './BaseResponseDataType';

export class TicketTariff implements BaseResponseDataType {
    id?: string;
    updatedAt?: Date;
    createdAt?: Date;
    title: string;
    overview: string;
    features: string[];
    isPerHour: boolean;
    isPerPerson: boolean;
    defaultPrice: number;


    constructor(createdAt: Date, id: string, updatedAt: Date, isPerHour: boolean, isPerPerson: boolean, defaultPrice: number, title: string, overview: string, features: string[]) {
        this.createdAt = createdAt;
        this.id = id;
        this.updatedAt = updatedAt;
        this.isPerPerson = isPerPerson;
        this.isPerHour = isPerHour;
        this.defaultPrice = defaultPrice;
        this.title = title;
        this.overview = overview;
        this.features = features;
    }

    public static mapToTicketTariff(ticketTariffJson: TicketTariffJson): TicketTariff {
        const featuresPart = new Array<string>();
        let titlePart: string;
        let overviewPart: string;

        ticketTariffJson.description.split(';;').forEach(descriptionPart => {
            if (descriptionPart.startsWith('[title]')) {
                titlePart = descriptionPart.replace(';;', '').replace('[title]', '');
            }

            if (descriptionPart.startsWith('[overview]')) {
                overviewPart = descriptionPart.replace(';;', '').replace('[overview]', '');
            }

            if (descriptionPart.startsWith('[features]')) {
                descriptionPart.replace('[features]', '').split(';').forEach(feature => {
                    featuresPart.push(feature.replace(';', ''));
                });
            }
        });

        return new TicketTariff(ticketTariffJson.createdAt,
            ticketTariffJson.id,
            ticketTariffJson.updatedAt,
            ticketTariffJson.isPerHour,
            ticketTariffJson.isPerPerson,
            ticketTariffJson.defaultPrice,
            titlePart,
            overviewPart,
            featuresPart);
    }

    public static mapFromTicketTariff(ticketTariff: TicketTariff): TicketTariffJson {
        const titlePart = `[title]${ticketTariff.title};;`;
        const overviewPart = `[overview]${ticketTariff.overview};;`;
        let featuresPart = '[features]';

        if (ticketTariff.features.length > 0) {
            ticketTariff.features.forEach(feature => {
                featuresPart += `${feature};`;
            });

            featuresPart += ';';
        } else {
            featuresPart += ';;';
        }

        const ticketTariffJson: TicketTariffJson = {
            createdAt: ticketTariff.createdAt,
            id: ticketTariff.id,
            updatedAt: ticketTariff.updatedAt,
            isPerHour: ticketTariff.isPerHour,
            isPerPerson: ticketTariff.isPerPerson,
            defaultPrice: ticketTariff.defaultPrice,
            description: titlePart + overviewPart + featuresPart
        };

        return ticketTariffJson;
    }
}

export interface TicketTariffJson {
    id?: string;
    updatedAt?: Date;
    createdAt?: Date;
    description: string;
    isPerHour: boolean;
    isPerPerson: boolean;
    defaultPrice: number;
}
