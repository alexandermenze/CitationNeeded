using CitationNeeded.Database.Database;
using CitationNeeded.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitationNeeded.WebApp
{
    public static class TestData
    {
        public static void Setup(CitationContext citationContext)
        {
            citationContext.Accounts.AddRange(
                new Account
                {
                    Id = "0",
                    Email = "test@thomastest.de",
                    FirstName = "Peter",
                    LastName = "Test",
                    HashedPassword = "ABCDE"
                },
                new Account
                {
                    Id = "1",
                    Email = "abc@home.pk",
                    FirstName = "Somebody",
                    LastName = "Once told me",
                    HashedPassword = "ABCDE"
                },
                new Account
                {
                    Id = "2",
                    Email = "help@ticketsystem.de",
                    FirstName = "Albert",
                    LastName = "Hanz",
                    HashedPassword = "ABCDE"
                },
                new Account
                {
                    Id = "3",
                    Email = "lars@arensmeier.de",
                    FirstName = "Lars-Peter",
                    LastName = "Arensmeier",
                    HashedPassword = "ABCDE"
                },
                new Account
                {
                    Id = "4",
                    Email = "ne@nein.de",
                    FirstName = "Nico",
                    LastName = "Dröge",
                    HashedPassword = "ABCDE"
                },
                new Account
                {
                    Id = "5",
                    Email = "henrik@schroedergmbh.de",
                    FirstName = "Henrik",
                    LastName = "Schröder",
                    HashedPassword = "ABCDEF"
                });

            citationContext.SaveChanges();

            citationContext.CitationBooks.AddRange(new CitationBook
            {
                Id = "0",
                Name = "TestBuch1",
                CitationGroups = new List<CitationGroup>
                {
                    new CitationGroup
                    {
                        Id = "0",
                        Created = DateTime.Now,
                        Author = citationContext.Accounts.Single(a => a.Id == "0"),
                        Citations = new[]
                        {
                            new Citation
                            {
                                Id = "0",
                                Speaker = "Hans Wurst",
                                Text = "Dies ist ein einzelnes Zitat!"
                            }
                        }
                    },
                    new CitationGroup
                    {
                        Id = "1",
                        Created = DateTime.Now.AddSeconds(12345),
                        Author = citationContext.Accounts.Single(a => a.Id == "2"),
                        Citations = new[]
                        {
                            new Citation
                            {
                                Id = "1",
                                Speaker = "Peter Hans",
                                Text = "Dies ist ein langes ZitatDies ist ein langes ZitatDies ist ein langes ZitatDies ist ein langes ZitatDies ist ein langes ZitatDies ist ein langes Zitat!"
                            }
                        }
                    },
                    new CitationGroup
                    {
                        Id = "2",
                        Created = DateTime.Now.AddSeconds(-123456),
                        Author = citationContext.Accounts.Single(a => a.Id == "2"),
                        Citations = new[]
                        {
                            new Citation
                            {
                                Id = "2",
                                Speaker = "Peter Hans",
                                Text = "Dies ist ein langes ZitatDies ist ein langes ZitatDies ist ein langes ZitatDies ist ein langes ZitatDies ist ein langes ZitatDies ist ein langes Zitat!"
                            }
                        }
                    },
                    new CitationGroup
                    {
                        Id = "3",
                        Created = DateTime.Now.AddSeconds(-123456),
                        Author = citationContext.Accounts.Single(a => a.Id == "3"),
                        Citations = new[]
                        {
                            new Citation
                            {
                                Id = "3",
                                Speaker = "Test Boyy",
                                Text = "Zitat Kevin"
                            }
                        }
                    },
                    new CitationGroup
                    {
                        Id = "4",
                        Created = DateTime.Now.AddSeconds(-1234576),
                        Author = citationContext.Accounts.Single(a => a.Id == "4"),
                        Citations = new[]
                        {
                            new Citation
                            {
                                Id = "4",
                                Speaker = "Test Boyy",
                                Text = "Mehrere Zitate!"
                            },
                            new Citation
                            {
                                Id = "5",
                                Speaker = "Other Guy",
                                Text = "Yess!"
                            },
                            new Citation
                            {
                                Id = "6",
                                Speaker = "Test Boyy",
                                Text = "Nummer 3!"
                            }
                        }
                    }
                }
            },
            new CitationBook
            {
                Id = "1",
                Name = "ITFA20LangerNameeeeee Yes",
                CitationGroups = new List<CitationGroup>
                {
                    new CitationGroup
                    {
                        Id = "5",
                        Created = DateTime.Now.AddSeconds(-123457677),
                        Author = citationContext.Accounts.Single(a => a.Id == "5"),
                        Citations = new[]
                        {
                            new Citation
                            {
                                Id = "7",
                                Speaker = "Mr Abcde",
                                Text = "Hallo!"
                            },
                            new Citation
                            {
                                Id = "8",
                                Speaker = "Hello World",
                                Text = "ABCDEFGHIJKL KLI!"
                            },
                            new Citation
                            {
                                Id = "9",
                                Speaker = "Hello World",
                                Text = "ABCDEFGHIJKL KLI!"
                            },
                            new Citation
                            {
                                Id = "10",
                                Speaker = "Hello World",
                                Text = "ABCDEFGHIJKL KLI!"
                            },
                            new Citation
                            {
                                Id = "11",
                                Speaker = "Mr Abcde",
                                Text = "Ok reicht!"
                            }
                        }
                    },
                    new CitationGroup
                    {
                        Id = "6",
                        Created = DateTime.Now.AddSeconds(-1236),
                        Author = citationContext.Accounts.Single(a => a.Id == "3"),
                        Citations = new[]
                        {
                            new Citation
                            {
                                Id = "12",
                                Speaker = "Ok Ok",
                                Text = "Ich habe kein Ideen mehr!"
                            }
                        }
                    },
                }
            });

            citationContext.SaveChanges();
        }
    }
}
