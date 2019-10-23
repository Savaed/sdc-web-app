declare global {
    interface Date {
        nowUtc(): Date;
        toUtc(date: Date): Date;
        isBefore(date: Date): boolean;
        dayToString(): string;
    }
}

// Returns datetime of current moment in UTC.
Date.prototype.nowUtc = () => {
    const date = new Date();
    const nowUtc = Date.UTC(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(),
        date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
    return new Date(nowUtc);
};

// Convert the given date to UTC.
Date.prototype.toUtc = (date: Date) => {
    const utcDate = Date.UTC(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(),
        date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
    return new Date(utcDate);
};

// Convert the weekday numeric representation to the appropriate string.
Date.prototype.dayToString = function(this: Date): string {
    const daysOfWeek = ['Sunday', 'Monday', 'Tuesday', ' Wednesday', 'Thursday', 'Friday', 'Saturday'];
    return daysOfWeek[(this.getUTCDay() + 1) % 7];
};

export { };
